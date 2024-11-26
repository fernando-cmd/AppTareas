﻿using Microsoft.EntityFrameworkCore;
using ToDoApp.Models; 

namespace ToDoApp.Data
{
    public class ToDoContext : DbContext
    {
        public ToDoContext(DbContextOptions<ToDoContext> options) : base(options) { }

        public DbSet<Tarea> Tareas { get; set; }  // La tabla de tareas
    }
}