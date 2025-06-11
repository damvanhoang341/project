using System;
using System.Collections.Generic;

namespace Server.Models;

public partial class Question
{
    public string Id { get; set; } = null!;

    public string Content { get; set; } = null!;

    public string? Correctanswer { get; set; }

    public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();

    public virtual ICollection<Test> Tests { get; set; } = new List<Test>();
    public Answer? SelectedAnswer { get; set; }
}
