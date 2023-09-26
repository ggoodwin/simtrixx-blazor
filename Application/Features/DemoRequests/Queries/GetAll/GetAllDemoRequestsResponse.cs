﻿namespace Application.Features.DemoRequests.Queries.GetAll
{
    public class GetAllDemoRequestsResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string? Notes { get; set; }
        public bool Contacted { get; set; }
    }
}
