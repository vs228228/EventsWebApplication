import { Component, OnInit } from '@angular/core';
import { UserService } from '../../services/user-service/user.service';
import { User } from '../../models/user.model';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrl: './user-profile.component.css'
})
export class UserProfileComponent implements OnInit {
  user: any;

  constructor(private userService: UserService) { }

  ngOnInit(): void {
    var id = Number(localStorage.getItem('userId'));
    this.userService.getUserById(id).then((data) => {
      this.user = data;
    });
  }
}
