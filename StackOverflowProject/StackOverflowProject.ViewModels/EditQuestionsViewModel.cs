﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace StackOverflowProject.ViewModels
{
    public class EditQuestionsViewModel
    {
        [Required]
        public int QuestionID { get; set; }

        [Required]
        public string QuestionName { get; set; }

        [Required]
        public DateTime QuestionDateAndTime { get; set; }

        [Required]
        public int CategoryID { get; set; }
    }
}
