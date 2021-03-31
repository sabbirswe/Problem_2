using AutoMapper;
using Problem_2.Database.Model;
using Problem_2.Utility.Model;

namespace Problem_2.Utility
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ModelMapper'
    public class ModelMapper : Profile
    {
        public ModelMapper()
        {

            CreateMap<Building, BuildingModel>();
            CreateMap<BuildingModel, Building>();

            CreateMap<Objects, ObjectsModel>();
            CreateMap<ObjectsModel, Objects>();

            CreateMap<DataField, DataFieldModel>();
            CreateMap<DataFieldModel, DataField>();

            CreateMap<Reading, ReadingModel>();
            CreateMap<ReadingModel, Reading>();

        }
    }
}
