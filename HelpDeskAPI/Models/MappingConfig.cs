using AutoMapper;


namespace HelpDeskAPI.Models.DTO
{
    public class MappingConfig
    {        
        public static void RegisterMaps()
        {

            Mapper.Initialize(config =>
            {
                config.ValidateInlineMaps = false;

                config.CreateMap<TSS_Tickets, TSS_TicketsDTO>()
                .ForMember(dest => dest.TicketPriorityDesc, opt => opt.MapFrom(src => src.CRM_LookupsPriorities.Description))
                .ForMember(dest => dest.TicketTypeDesc, opt => opt.MapFrom(src => src.CRM_LookupsTicketsTypes.Description))
                .ForMember(dest => dest.BgColor, opt => opt.MapFrom(src => src.CRM_LookupsPriorities.BgColor))
                .ForMember(dest => dest.FgColor, opt => opt.MapFrom(src => src.CRM_LookupsPriorities.FgColor))
                .ForMember(dest => dest.TicketStatusDesc, opt => opt.MapFrom(src => src.CRM_LookupsTicketsStatuses.Description))
                .ForMember(dest => dest.ModuleDesc, opt => opt.MapFrom(src => src.Module.Description))
                .ForMember(dest => dest.TicketClientName, opt => opt.MapFrom(src => src.TSS_Clients.ClientsName));


                config.CreateMap<TSS_TicketsDTO, TSS_Tickets>();

                config.CreateMap<TSS_TicketsActions, TSS_TicketsActionsDTO>();
                config.CreateMap<TSS_TicketsActionsDTO, TSS_TicketsActions>();

                config.CreateMap<TSS_TicketsAttachedFiles, TSS_TicketsAttachedFilesDTO>();
                config.CreateMap<TSS_TicketsAttachedFilesDTO, TSS_TicketsAttachedFiles>();
            });
        }
    }
}