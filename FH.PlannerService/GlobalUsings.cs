global using Microsoft.EntityFrameworkCore;
global using System.ComponentModel.DataAnnotations;
global using Microsoft.Extensions.Options;
global using RabbitMQ.Client;
global using RabbitMQ.Client.Events;
global using System.Text;
global using System.Text.Json;
global using AutoMapper;
global using System.Text.Json.Serialization;

global using FH.PlannerService.Data;
global using FH.PlannerService.Models;
global using FH.PlannerService.EventProcessing;
global using FH.PlannerService.Dtos;
global using FH.PlannerService.Dtos.IncomingEventDtos;

global using FH.EventProcessing;
global using FH.EventProcessing.Helpers;
global using FH.EventProcessing.Dtos;
global using FH.EventProcessing.Config;
