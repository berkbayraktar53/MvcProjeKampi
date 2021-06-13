using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class StatisticManager : IStatisticService
    {
        ICategoryDal _categoryDal;
        IHeadingDal _headingDal;
        IWriterDal _writerDal;
        public StatisticManager(ICategoryDal categoryDal, IHeadingDal headingDal, IWriterDal writerDal)
        {
            _categoryDal = categoryDal;
            _headingDal = headingDal;
            _writerDal = writerDal;
        }

        public List<Heading> GetHeadingList()
        {
            return _headingDal.List();
        }

        public List<Category> GetCategoryList()
        {
            return _categoryDal.List();
        }

        public List<Writer> GetWriterList()
        {
            return _writerDal.List();
        }
    }
}