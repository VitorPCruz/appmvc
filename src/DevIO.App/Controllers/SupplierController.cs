using Microsoft.AspNetCore.Mvc;
using DevIO.App.ViewModels;
using DevIO.Business.Interfaces;
using AutoMapper;
using DevIO.Business.Models;

namespace DevIO.App.Controllers
{
    public abstract class BaseController : Controller
    {

    }


    public class SupplierController : BaseController
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IMapper _mapper;

        public SupplierController(ISupplierRepository supplierRepository, IMapper mapper)
        {
            _supplierRepository = supplierRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<SupplierViewModel>>(
                await _supplierRepository.GetAllEntities()));
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var supplierViewModel = await GetSupplierAddress(id);

            if (supplierViewModel == null)
                return NotFound();

            return View(supplierViewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SupplierViewModel supplierViewModel)
        {
            if (!ModelState.IsValid) 
                return View(supplierViewModel);

            var supplier = _mapper.Map<Supplier>(supplierViewModel);
            await _supplierRepository.AddEntity(supplier);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var supplierViewModel = await GetSupplierProductsAddress(id);
            
            if (supplierViewModel == null)
                return NotFound();
            
            return View(supplierViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, SupplierViewModel supplierViewModel)
        {
            if (id != supplierViewModel.Id)
                return NotFound();

            if (!ModelState.IsValid) 
                return View(supplierViewModel);

            var supplier = _mapper.Map<Supplier>(supplierViewModel);

            await _supplierRepository.UpdateEntity(supplier);
            
            return RedirectToAction("Index");
            
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var supplierViewModel = await GetSupplierAddress(id);
            
            if (supplierViewModel == null)
                return NotFound();

            return View(supplierViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var supplierViewModel = await GetSupplierAddress(id);

            if (supplierViewModel == null)
                return NotFound();

            await _supplierRepository.RemoveEntity(id);

            return RedirectToAction(nameof(Index));
        }

        private async Task<SupplierViewModel> GetSupplierAddress(Guid supplierId)
        {
            return _mapper.Map<SupplierViewModel>(
                await _supplierRepository.GetSupplierAddress(supplierId));
        }

        private async Task<SupplierViewModel> GetSupplierProductsAddress(Guid supplierId)
        {
            return _mapper.Map<SupplierViewModel>(
                await _supplierRepository.GetSupplierProductsAddress(supplierId));
        }
    }
}
