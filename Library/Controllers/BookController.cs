using Microsoft.AspNetCore.Mvc;
using Library.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft .EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;  // allows us to authorize users
using Microsoft.AspNetCore.Identity; // allows this controller to interact with users from the db
using System.Threading.Tasks; // allows us to call async methods
using System.Security.Claims; // allows claim based authorization

