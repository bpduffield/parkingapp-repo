using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiscussionMVCAppDuffield.Models
{
    public interface IDepartmentRepo
    {
        List<Department> ListAllDepartments();
    }
}
