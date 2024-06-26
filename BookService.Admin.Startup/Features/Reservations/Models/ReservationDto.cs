﻿using BookService.Domain.Models.Enums;

namespace BookService.Admin.Startup.Features.Reservations.Models;

public class ReservationDto : CreateReservationDto
{
    public long Id { get; set; }
    public long ReservedPlacesCount { get; set; }
    public ReservationStatus Status { get; set; }
    public string RestaurantName { get; set; }
    public string TableName { get; set; }
}