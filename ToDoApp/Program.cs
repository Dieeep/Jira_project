using Microsoft.EntityFrameworkCore;
using ToDoApp.Data.Context;
using ToDoApp.Data.Models;

var optionsBuilder = new DbContextOptionsBuilder<ToDoContext>();
optionsBuilder.UseNpgsql("Host=localhost;Database=vikahykava;Username=postgres;Password=07082004");
var context = new ToDoContext(optionsBuilder.Options);

