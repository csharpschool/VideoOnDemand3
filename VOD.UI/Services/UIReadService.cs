using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VOD.Database.Services;

namespace VOD.UI.Services
{
    public class UIReadService : IUIReadService
    {
        #region Properties
        private readonly IDbReadService _db;
        #endregion

        #region Constructor
        public UIReadService(IDbReadService db)
        {
            _db = db;
        }
        #endregion

        #region Methods
        #endregion

    }
}
