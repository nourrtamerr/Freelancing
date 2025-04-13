﻿using AutoMapper;
using Freelancing.DTOs;

namespace Freelancing.Helpers
{
    public class ChatProfile : Profile
    {
        public ChatProfile() {
            CreateMap<Chat, ChatDto>()
                         .ForMember(dest => dest.SenderName, opt => opt.MapFrom(src => src.Sender != null ? src.Sender.UserName : "Unknown"))
                         .ForMember(dest => dest.ReceiverName, opt => opt.MapFrom(src => src.Receiver != null ? src.Receiver.UserName : "Unknown"));
            CreateMap<CreateChatDto, Chat>();

        }
    }
}
