using Company.G01.BLL.Interfaces;
using Company.G01.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Company.G01.PL.Controllers
{
    [Authorize]
    public class EmployeeProjectController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeProjectController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            var employeeproject = await _unitOfWork.EmployeeProjectRepository.GetAllAsync();
            return View(employeeproject);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null) return BadRequest();
            var employeeproject = await _unitOfWork.EmployeeProjectRepository.GetAsync(id.Value);
            if (employeeproject is null) return NotFound();
            return View(employeeproject);
        }

        #region Create

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var employees = await _unitOfWork.EmployeeRepository.GetAllAsync();
            var projects = await _unitOfWork.ProjectRepository.GetAllAsync();
            ViewData["Employee"] = employees;
            ViewData["Project"] = projects;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeProject model)
        {
            if (ModelState.IsValid)
            {
               
           
                await _unitOfWork.EmployeeProjectRepository.AddAsync(model);
                var count = await _unitOfWork.CompleteAsync();
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);

        }
        #endregion

        #region Update
        [HttpGet]

        public async Task<IActionResult> Update(int? id)
        {
            if (id is null) return BadRequest();
            var employeeProject = await _unitOfWork.EmployeeProjectRepository.GetAsync(id.Value);
            if (employeeProject is null) return NotFound();
            var Employee = await _unitOfWork.EmployeeRepository.GetAllAsync();
            var Project = await _unitOfWork.ProjectRepository.GetAllAsync();
            ViewData["Employee"] = Employee;
            ViewData["Project"] = Project;
            return View(employeeProject);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromRoute] int? id, EmployeeProject model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _unitOfWork.EmployeeProjectRepository.Update(model);
                    var count = await _unitOfWork.CompleteAsync();
                    if (count > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }

            }
            catch (Exception EX)
            {
                ModelState.AddModelError(string.Empty, EX.Message);
            }

            return View(model);
        }
        #endregion

        #region Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {

            if (id is null) return BadRequest();
            var employeeProject = await _unitOfWork.EmployeeProjectRepository.GetAsync(id.Value);
            if (employeeProject is null) return NotFound();
            return View(employeeProject);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Delete(EmployeeProject model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _unitOfWork.EmployeeProjectRepository.Delete(model);
                    var count = await _unitOfWork.CompleteAsync();
                    if (count > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }

            }
            catch (Exception EX)
            {
                ModelState.AddModelError(string.Empty, EX.Message);
            }

            return View(model);
        }
        #endregion

    }
}
