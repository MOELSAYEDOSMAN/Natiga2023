using DataShare;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Natiga2023.Models;

namespace Natiga2023.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class NatigaController : ControllerBase
    {
        private readonly Natiga2023Context db;
        public NatigaController(Natiga2023Context _db)
        {
            db = _db;
        }
        [HttpPost]
        public async Task<List<BackToFront>> GetNatiga([FromBody]FrontToBack input)
        {
            if(ModelState.IsValid)
            {
                if(input.Type== "Number")
                {
                    var ng = db.Stage_New_Search_s.SingleOrDefault(x=>x.seating_no==double.Parse(input.NameOrNuber));
                    if (ng != null)
                        return new() { new BackToFront() {
                            search = true,
                            Number=ng?.seating_no.ToString()??"",
                            Name=ng?.arabic_name??"",
                            State=ng?.student_case_desc,
                            Grade=ng?.total_degree??0,
                            Grader100=(100*((ng?.total_degree??0)/410)),
                            Arg=0
                        } };
                    else
                        return new() { new BackToFront() { search = true } };
                }
                else
                {
                    var ng = db.Stage_New_Search_s.Where(x => x.arabic_name.Contains(input.NameOrNuber)).ToList();
                    List<BackToFront> list = new List<BackToFront>();
                    foreach (var ng2 in ng)
                    {
                        BackToFront b = new()
                        {
                            search = true,
                            Number = ng2.seating_no.ToString(),
                            Name = ng2?.arabic_name ?? "",
                            State = ng2?.student_case_desc,
                            Grade = ng2?.total_degree ?? 0,
                            Grader100 = (100 * ((ng2?.total_degree ?? 0) / 410)),
                            Arg = 0
                        };
                        list.Add(b);
                    }
                    return list;
                }
            }
            return new() { new BackToFront() { search = false } };
        }
        }
    }

