using System.ComponentModel.DataAnnotations;

namespace EventsWebApplication.Server.Application.DTOs
{
    public class DateOnlyDto
    {
        public int Year { get; set; }

        public int Month { get; set; }

        public int Day { get; set; }

    }
}
