using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.BL.Models;
using University.BL.Repositories;

namespace University.BL.Services.Implements
{
    public class OfficeAssignmentServicie : GenericService<OfficeAssignment>, IOfficeAssignmentService
    {
        public OfficeAssignmentServicie(IOfficeAssignmentRepository officeAssignmentRepository) : base(officeAssignmentRepository)
        {

        }
    }
}
