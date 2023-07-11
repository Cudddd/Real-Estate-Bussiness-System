using BDS.Data.EF;
using BDS.Services.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDS.Services.RealEstate
{
    public abstract class RealEstateModelBuilder
    {
        protected readonly BdsDbContext _context;
        public RealEstateModelBuilder(BdsDbContext context)
        {
            _context = context;
        }

        protected RealEstateModel RealEstateModel { get; private set; }

        public RealEstateModel Build(Data.Entities.RealEstate item)
        {
            RealEstateModel = new RealEstateModel();

            SetBasicInfo(item);
            SetAdditionalInfo(item);
            SetRealEstateMedia(item);

            return RealEstateModel;
        }

        protected abstract void SetBasicInfo(Data.Entities.RealEstate item);

        protected virtual void SetAdditionalInfo(Data.Entities.RealEstate item)
        {
        }

        protected virtual void SetRealEstateMedia(Data.Entities.RealEstate item)
        {
            RealEstateModel.realEstateMedia = _context.RealEstateMedia
                .Where(x => x.RealEstateId == RealEstateModel.id).ToList();
        }
    }

    public class BasicRealEstateModelBuilder : RealEstateModelBuilder
    {
        public BasicRealEstateModelBuilder(BdsDbContext context) : base(context)
        {
        }

        protected override void SetBasicInfo(Data.Entities.RealEstate item)
        {
            RealEstateModel.id = item.id;
            RealEstateModel.areaID = item.areaID;
            RealEstateModel.name = item.name;
            RealEstateModel.acreage = item.acreage;
            RealEstateModel.bathRoom = item.bathRoom;
            RealEstateModel.bedRoom = item.bedRoom;
            RealEstateModel.DateCreated = item.DateCreated;
            RealEstateModel.DateModify = item.DateModify;
            RealEstateModel.facade = item.facade;
            RealEstateModel.floor = item.floor;
            RealEstateModel.length = item.length;
            RealEstateModel.width = item.width;
            RealEstateModel.orientation = item.orientation;
            RealEstateModel.price = item.price;
            RealEstateModel.location = item.location;
            RealEstateModel.mainLine = item.mainLine;
            RealEstateModel.sell = item.sell;
            RealEstateModel.type = _context.RealEstateType.FirstOrDefault(x => x.id == item.typeID)?.name;
        }
    }

    public class ExtendedRealEstateModelBuilder : BasicRealEstateModelBuilder
    {
        public ExtendedRealEstateModelBuilder(BdsDbContext context) : base(context)
        {
        }

        protected override void SetBasicInfo(Data.Entities.RealEstate item)
        {
            base.SetBasicInfo(item);
            RealEstateModel.description = item.description;
            RealEstateModel.address = item.address;
        }

        protected override void SetAdditionalInfo(Data.Entities.RealEstate item)
        {
        }
    }

}
