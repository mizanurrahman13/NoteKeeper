﻿namespace NOTEKEEPER.Api.Entities;

public class Note
{
    public int Id { get; set; }
    public string Text { get; set; }
    public string Type { get; set; }
    public DateTime? Reminder { get; set; }
    public DateTime? DueDate { get; set; }
    public bool IsComplete { get; set; }
    public string Url { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
}
