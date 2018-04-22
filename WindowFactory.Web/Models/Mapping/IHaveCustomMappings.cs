using AutoMapper;

namespace WindowFactory.Web.Models.Mapping
{
    public interface IHaveCustomMappings
    {
        void CreateMappings(IConfiguration configuration);
    }
}
