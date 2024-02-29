using clutch_position.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace clutch_position.Requests
{
    public class PatchPositionStateRequest
    {
        public PositionStatus positionStatus{get;set;}
    }
}