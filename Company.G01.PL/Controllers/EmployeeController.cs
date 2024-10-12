using AutoMapper;
using Company.G01.BLL.Interfaces;
using Company.G01.DAL.Models;
using Company.G01.PL.Helpers;
using Company.G01.PL.ViewModels.Employee;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;

namespace Company.G01.PL.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        //private readonly IEmployeeRepository _employeeRepository;
        //private readonly IDepartmentRepository _departmentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeController(
            //IEmployeeRepository employeeRepository,
            //IDepartmentRepository departmentRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            //_employeeRepository = employeeRepository;
            //_departmentRepository = departmentRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(string InputSearch)
        {
            var employees = Enumerable.Empty<Employee>();

            if(string.IsNullOrEmpty(InputSearch))
            {
                 employees = await _unitOfWork.EmployeeRepository.GetAllAsync();

            }
            else
            {
                 employees = await _unitOfWork.EmployeeRepository.GetByNameAsync(InputSearch);
            }
            var result = _mapper.Map<IEnumerable<EmployeeViewModel>>(employees);
            // View's Dictionary : Transfer Data From Action To View (One Ways)

            // 1. ViewData : Property Inherted From Controller Class, Dictionary
            //ViewData["Data01"] = "Hello World From ViewData";

            // 2. ViewBag : Property Inherted From Controller Class, Dictionary
            //ViewBag.Data02 = "Hello World From ViewBag";

            // 3. TempData : Property Inherted From Controller Class, Dictionary
            // Transfer Data From Request To Another Request
            // TempData["Data03"] = "Hello World From TempData";


            return View(result);
        }

        public async Task<IActionResult> Details(int? id, string viewname = "Details")
        {
            if (id is null)
            {
                return BadRequest();
            }

            var employee = await _unitOfWork.EmployeeRepository.GetAsync(id.Value);

            if (employee is null)
            {
                return NotFound();
            }
            // Mapping Employee ===> EmployeeViewModel
            //EmployeeViewModel employeeviewmodel = new EmployeeViewModel()
            //{
            //    id = employee.id,
            //    Name = employee.Name,
            //    Address = employee.Address,
            //    Email = employee.Email,
            //    Salary = employee.Salary,
            //    Age = employee.Age,
            //    HireDate = employee.HireDate,
            //    IsActived = employee.IsActived,
            //    WorkFor = employee.WorkFor,
            //    WorkForId = employee.WorkForId,
            //    PhoneNumber = employee.PhoneNumber,
            //};

            // Auto mapping

            var employeeviewmodel = _mapper.Map<EmployeeViewModel>(employee);

            return View(employeeviewmodel);
        }

        #region Create

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var Department =await _unitOfWork.DepartmentRepository.GetAllAsync();
            ViewData["Department"] = Department;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                
                try
                {
                    if (model.ImageName is not null)
                    {
                        model.ImageName = DocumentSettings.UploadFile(model.Image, "images");
                    }
                    // Casting EmployeeViewModel (ViewModel) -----> Employee (Model)

                    // Mapping :
                    // 1. Manual Mapping

                    //Employee employee = new Employee()
                    //{
                    //    id = model.id,
                    //    Name = model.Name,
                    //    Address = model.Address,
                    //    Email = model.Email,
                    //    Salary = model.Salary,
                    //    Age = model.Age,
                    //    HireDate = model.HireDate,
                    //    IsActived = model.IsActived,
                    //    WorkFor = model.WorkFor,
                    //    WorkForId = model.WorkForId,
                    //    PhoneNumber = model.PhoneNumber,
                    //};


                    // 2. Auto Mapping


                    var employee = _mapper.Map<Employee>(model);


                    await _unitOfWork.EmployeeRepository.AddAsync(employee);
                    var Count =await _unitOfWork.CompleteAsync();

                    if (Count > 0)
                    {
                        TempData["Message"] = $"Employee {model.Name} Created!";
                    }
                    else
                    {
                        TempData["Message"] = $"Employee {model.Name} Not Created!";
                    }
                   
                        return  RedirectToAction(nameof(Index));
              
                }
                catch (Exception Ex)
                {
                    ModelState.AddModelError(string.Empty, Ex.Message);
                }
            }

            return View(model);
        }
        #endregion

        #region Update 
        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            var Department =await _unitOfWork.DepartmentRepository.GetAllAsync();
            ViewData["Department"] = Department;
            return await  Details(id, nameof(Update));
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // With Action Post (Request from web me only)
        public async Task<IActionResult> Update([FromRoute] int? id, EmployeeViewModel model)
        {
            try
            {
                if (id != model.id) return BadRequest();
                if (ModelState.IsValid)
                {
                    if(model.ImageName is not null)
                    {
                        DocumentSettings.DeleteFile(model.ImageName, "images");

                    }
                    if(model.Image is not null)
                    {
                        model.ImageName = DocumentSettings.UploadFile(model.Image, "images");

                    }
                    //Employee employee = new Employee()
                    //{
                    //    id = model.id,
                    //    Name = model.Name,
                    //    Address = model.Address,
                    //    Email = model.Email,
                    //    Salary = model.Salary,
                    //    Age = model.Age,
                    //    HireDate = model.HireDate,
                    //    IsActived = model.IsActived,
                    //    WorkFor = model.WorkFor,
                    //    WorkForId = model.WorkForId,
                    //    PhoneNumber = model.PhoneNumber,
                    //};

                    var employee = _mapper.Map<Employee>(model);

                     _unitOfWork.EmployeeRepository.Update(employee);
                    var employee1 =await  _unitOfWork.CompleteAsync();

                    return RedirectToAction("Index");
                }
            }

            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }



            return View(model);
        }

        #endregion

        #region Delete
        // Hard Delete : Delete from database
        // Soft Delete : Update the status of the record to inactive And Data found

        [HttpGet]
        public Task<IActionResult> Delete(int? id)
        {

            return Details(id, nameof(Delete));
        }


        [HttpPost]
        [ValidateAntiForgeryToken] // With Action Post ( Request from web me only )
        public async Task<IActionResult> Delete([FromRoute] int? id, EmployeeViewModel model)
        {
            try
            {
                if (id != model.id) return BadRequest();
                if (ModelState.IsValid) // Server Side Validation
                {
                    if (model.ImageName is not null)
                    {
                        DocumentSettings.DeleteFile(model.ImageName, "images");

                    }
                    //Employee employee = new Employee()
                    //{
                    //    id = model.id,
                    //    Name = model.Name,
                    //    Address = model.Address,
                    //    Email = model.Email,
                    //    Salary = model.Salary,
                    //    Age = model.Age,
                    //    HireDate = model.HireDate,
                    //    IsActived = model.IsActived,
                    //    WorkFor = model.WorkFor,
                    //    WorkForId = model.WorkForId,
                    //    PhoneNumber = model.PhoneNumber,
                    //};

                    var employee = _mapper.Map<Employee>(model);

                    _unitOfWork.EmployeeRepository.Delete(employee);
                    var employee1 = await _unitOfWork.CompleteAsync();

                    TempData["T1"] = $"{model.Name} Deleted Successfuly!";
                    return RedirectToAction("Index");
                }
            }

            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(model);
        }
        #endregion
    }
}
