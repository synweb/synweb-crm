using System;
using System.Collections.Generic;
using System.Text;

namespace SynWebCRM.Data.EF
{
    public abstract class BaseRepository
    {
        private readonly CRMModel _crmModel;

        public BaseRepository(CRMModel crmModel)
        {
            _crmModel = crmModel;
        }

    }
}
