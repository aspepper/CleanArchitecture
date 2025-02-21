﻿using AcadesArchitecturePattern.Application.Handlers.Events;
using AcadesArchitecturePattern.Domain.Events;
using AcadesArchitecturePattern.Domain.Interfaces;
using AcadesArchitecturePattern.Domain.Queries.Users;
using AcadesArchitecturePattern.Shared.Queries;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace AcadesArchitecturePattern.Application.Handlers.Users;

public class SearchUserByIdHandle : IRequestHandler<SearchUserByIdQuery, GenericQueryResult>
{
    private readonly IUserService userService;
    private readonly ILogger<ToDoListEventHandle> logger;
    private readonly IMediator mediator;
    private readonly HttpClient client;
    private readonly IConfiguration configuration;

    public SearchUserByIdHandle(IUserService userService, ILogger<ToDoListEventHandle> logger, IMediator mediator, IConfiguration config)
    {
        this.userService = userService;
        this.logger = logger;
        this.mediator = mediator;
        configuration = config;

        if (client == null)
        {
            client = new HttpClient
            {
                BaseAddress = new Uri($"{configuration["APIConfigProtocol"]}://{configuration["APIConfigServer"]}/{configuration["APIConfigURL"]}/")
            };
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

    }

    public async Task<GenericQueryResult> Handle(SearchUserByIdQuery query, CancellationToken cancellationToken)
    {
        try
        {
            query.Validate();

            if (!query.IsValid)
            {
                return await Task.FromResult(new GenericQueryResult(false, "Insira corretamente os dados do usuário", query.Notifications));
            }

            HttpResponseMessage response = client.GetAsync($"/{configuration["APIConfigSystem"]}/{configuration["APIConfigCompany"]}", cancellationToken).Result;

            if (!response.IsSuccessStatusCode)
            { throw new Exception("Usuário não encontrado"); }

            var searchedUser = response.Content.ReadFromJsonAsync<Domain.Entities.User>(cancellationToken: cancellationToken).Result ?? throw new Exception("Usuário não encontrado");

            logger.LogInformation("Tarefa Concluída: {CommandName}", query.GetType().Name);

            var userEvent = new UserEvent(searchedUser);
            await mediator.Publish(userEvent, cancellationToken);

            return await Task.FromResult(new GenericQueryResult(true, "Usuários encontrados!", searchedUser));
        }
        catch (Exception ex)
        {
            logger.LogInformation("Tarefa Falhou: {CommandName}", query.GetType().Name);
            return await Task.FromResult(new GenericQueryResult(false, ex.Message, query.GetType().Name));
        }
    }
}
