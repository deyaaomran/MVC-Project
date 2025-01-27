using Company.G01.BLL.Interfaces;
using Company.G01.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Company.G01.PL.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProjectController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            var project = await _unitOfWork.ProjectRepository.GetAllAsync();
            return View(project);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null) return BadRequest();
            var project = await _unitOfWork.ProjectRepository.GetAsync(id.Value);
            if (project is null) return NotFound();
            return View(project);
        }

        #region Create
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Project model)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.ProjectRepository.AddAsync(model);
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
            var project = await _unitOfWork.ProjectRepository.GetAsync(id.Value);
            if (project is null) return NotFound();
            return View(project);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromRoute] int? id, Project model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _unitOfWork.ProjectRepository.Update(model);
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
            var project = await _unitOfWork.ProjectRepository.GetAsync(id.Value);
            if (project is null) return NotFound();
            return View(project);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Delete(Project model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _unitOfWork.ProjectRepository.Delete(model);
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
