﻿using Base.Api.Application.Dtos.Wrappers;
using MediatR;
using System.Collections.Generic;

namespace Base.Api.Application.Dtos.Notes;

public class GetMyNotesByCategoryId : IRequest<Response<List<NoteDto>>>
{
    public string Id { get; set; }
}