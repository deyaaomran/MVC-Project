using Company.G01.BLL.Interfaces;
using Company.G01.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Company.G01.PL.Controllers
{
    [Authorize]

    public class PositionController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public PositionController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var pos = await _unitOfWork.PositionRepository.GetAllAsync();
            return View(pos);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null) return BadRequest();
            var pos = await _unitOfWork.PositionRepository.GetAsync(id.Value);
            if (pos is null) return NotFound();
            return View(pos);
        }

        #region Create
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Position model)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.PositionRepository.AddAsync(model);
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
            var pos = await _unitOfWork.PositionRepository.GetAsync(id.Value);
            if (pos is null) return NotFound();
            return View(pos);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromRoute] int? id, Position model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _unitOfWork.PositionRepository.Update(model);
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
            var pos = await _unitOfWork.PositionRepository.GetAsync(id.Value);
            if (pos is null) return NotFound();
            return View(pos);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Delete(Position model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _unitOfWork.PositionRepository.Delete(model);
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
