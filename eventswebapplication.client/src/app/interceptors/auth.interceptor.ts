import { Injectable } from '@angular/core';
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError, from } from 'rxjs';
import { catchError, switchMap } from 'rxjs/operators';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth-service/auth.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(private authService: AuthService, private router: Router) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return from(this.authService.getAccessToken()).pipe(
      switchMap((accessToken) => {
        let authReq = req;

        // Добавляем accessToken в заголовок запроса, если он существует
        if (accessToken) {
          authReq = req.clone({
            setHeaders: {
              Authorization: `Bearer ${accessToken}`
            }
          });
        }

        return next.handle(authReq).pipe(
          catchError((error: HttpErrorResponse) => {
            // Если получаем ошибку 401, пытаемся обновить токены
            if (error.status === 401 && !authReq.url.includes('/refreshToken')) {
              return from(this.authService.refreshTokens()).pipe(
                switchMap((tokenRefreshed: boolean) => {
                  if (tokenRefreshed) {
                    // Повторяем запрос с новым accessToken
                    return from(this.authService.getAccessToken()).pipe(
                      switchMap((newAccessToken) => {
                        const newAuthReq = req.clone({
                          setHeaders: {
                            Authorization: `Bearer ${newAccessToken}`
                          }
                        });
                        return next.handle(newAuthReq);
                      })
                    );
                  } else {
                    this.authService.logout();
                    this.router.navigate(['/login']);
                    return throwError(() => new Error('Unauthorized'));
                  }
                }),
                catchError(() => {
                  // Если обновление токенов не удалось, перенаправляем на страницу входа
                  this.authService.logout();
                  this.router.navigate(['/login']);
                  return throwError(() => new Error('Unauthorized'));
                })
              );
            }

            return throwError(() => error);
          })
        );
      })
    );
  }
}
