using Company.G01.BLL.Interfaces;
using Company.G01.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Company.G01.PL.Controllers
{
    [Authorize]
    public class SalaryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public SalaryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<IActionResult> Index()
        {
            var salary = await _unitOfWork.SalaryRepository.GetAllAsync();
            return View(salary);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null) return BadRequest();
            var salary = await _unitOfWork.SalaryRepository.GetAsync(id.Value);
            if (salary is null) return NotFound();
            return View(salary);
        }

        #region Create

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var employees = await _unitOfWork.EmployeeRepository.GetAllAsync();  
            ViewData["Employee"] = employees;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Salary model)
        {
            if (ModelState.IsValid)
            {


                await _unitOfWork.SalaryRepository.AddAsync(model);
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
        
        
        public async Task<IActionResult> Update( int? id)
        {
            if (id is null) return BadRequest();
            var salary = await _unitOfWork.SalaryRepository.GetAsync(id.Value);
            var Employee = await _unitOfWork.EmployeeRepository.GetAllAsync();
            ViewData["Employee"] = Employee;
            if (salary is null) return View(nameof(Create));
            
            return View(salary);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromRoute] int? id, Salary model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _unitOfWork.SalaryRepository.Update(model);
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
            var salary = await _unitOfWork.SalaryRepository.GetAsync(id.Value);
            if (salary is null) return NotFound();
            return View(salary);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Delete(Salary model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _unitOfWork.SalaryRepository.Delete(model);
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
