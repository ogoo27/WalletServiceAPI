using AutoMapper;
using E_WalletAPI.Models;
using E_WalletAPI.Models.DTO;

namespace E_WalletAPI.Profiles
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<RegisterNewAccountModel, AccountModel>();
            CreateMap<UpdateAccountModel, AccountModel>();
            CreateMap<AccountModel, GetAccountModel>();
            CreateMap<TransactionRequestDTO, TransactionModel>();
        }
       
    }
}
