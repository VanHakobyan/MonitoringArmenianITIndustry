﻿using System;
using System.Collections.Generic;

namespace MonitoringIT.DAL.Models
{
    public partial class LinkedinLanguage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LinkedinProfileId { get; set; }

        public LinkedinProfile LinkedinProfile { get; set; }
    }
}