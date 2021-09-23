﻿using System;

namespace Banking.Operation.Transfer.Command.Domain.Transfer.Dtos
{
    public class ContactDto
    {
        public ContactDto()
        {
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Account { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
    }
}
