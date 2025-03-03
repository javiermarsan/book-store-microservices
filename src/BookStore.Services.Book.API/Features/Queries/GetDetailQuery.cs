﻿using AutoMapper;
using BookStore.Services.Book.API.Infrastructure;
using BookStore.Services.Book.API.Infrastructure.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Services.Book.API.Features.Queries
{
    public class GetDetailQuery : IRequest<BookDto>
    {
        public Guid? BookId { get; set; }

        public class GetDetailQueryHandler : IRequestHandler<GetDetailQuery, BookDto>
        {
            private readonly BookContext _context;
            private readonly IMapper _mapper;

            public GetDetailQueryHandler(BookContext contexto, IMapper mapper)
            {
                _context = contexto;
                _mapper = mapper;
            }

            public async Task<BookDto> Handle(GetDetailQuery request, CancellationToken cancellationToken)
            {
                var entity = await _context.Book.Where(x => x.BookId == request.BookId).FirstOrDefaultAsync();
                if (entity == null)
                    throw new Exception("Book not found");

                var dto = _mapper.Map<BookEntity, BookDto>(entity);
                return dto;
            }
        }
    }
}
