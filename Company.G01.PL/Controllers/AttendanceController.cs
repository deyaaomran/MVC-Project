using Company.G01.BLL.Interfaces;
using Company.G01.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Company.G01.PL.Controllers
{
    [Authorize]
    public class AttendanceController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;

		public AttendanceController( IUnitOfWork unitOfWork)
        {
			_unitOfWork = unitOfWork;
		}

        [HttpGet]
        public async Task<IActionResult> Index()
		{
			var attend = await _unitOfWork.AttendanceRepository.GetAllAsync();
			return View(attend);
		}

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var Employee = await _unitOfWork.EmployeeRepository.GetAllAsync();
            ViewData["Employee"] = Employee;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Attendance model)
        {
            if (ModelState.IsValid)
            {
                if (model.Status == "Absent")
                {
                    model.CheckOutTime = null;
                    model.CheckInTime = null;
                }
                model.Date = DateTime.Now.Date;
                await _unitOfWork.AttendanceRepository.AddAsync(model);
                var count = await _unitOfWork.CompleteAsync();
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);

        }
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null) return BadRequest();
            var attend = await _unitOfWork.AttendanceRepository.GetAsync(id.Value);
            if (attend is null) return NotFound();
            return View(attend);
        }

        [HttpGet]

        public async Task<IActionResult> Update(int? id)
        {
            if (id is null) return BadRequest();
            var attend = await _unitOfWork.AttendanceRepository.GetAsync(id.Value);
            if (attend is null) return NotFound();
            var Employee = await _unitOfWork.EmployeeRepository.GetAllAsync();
            ViewData["Employee"] = Employee;
            return View(attend);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromRoute] int? id, Attendance model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _unitOfWork.AttendanceRepository.Update(model);
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


        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {

            if (id is null) return BadRequest();
            var attend = await _unitOfWork.AttendanceRepository.GetAsync(id.Value);
            if (attend is null) return NotFound();
            return View(attend);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Delete(Attendance model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _unitOfWork.AttendanceRepository.Delete(model);
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
    }
}
