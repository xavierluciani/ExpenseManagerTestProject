
namespace Expense.Services.Mapping
{
    using AutoMapper;
    using Expense.DTO;
    using Expense.Entities.Models;

    /// <summary>
    /// Automapper profile to convert entities to dto
    /// </summary>
    public class ExpenseProfile : Profile
    {
        /// <summary>
        /// Constructor of <see cref="ExpenseProfile"/>.
        /// </summary>
        public ExpenseProfile()
        {
            CreateMap<Expense, ExpenseDto>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => $"{src.User.Surname} {src.User.Name}"))
            .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => $"{src.User.Currency.CurCode}"));

            CreateMap<ExpenseDto, Expense>()
            .ForMember(dest => dest.User, opt => opt.Ignore());

            CreateMap<NatureDto, Nature>()
            .ForMember(dest => dest.Expenses, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
