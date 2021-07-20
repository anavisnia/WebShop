using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebShop.Authorization;
using WebShop.Dtos;
using WebShop.Interfaces;

namespace WebShop.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ShopController : ControllerBase
    {
        private readonly IShopRepository _repository;
        private readonly IMapper _mapper;

        public ShopController(IShopRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<List<ShopDto>> GetAll()
        {
            return await _repository.GetAll();
        }

        [HttpPost]
        public async Task Create(ShopDto shopDto)
        {
            if (shopDto == null)
            {
                throw new ArgumentNullException();
            }
            await _repository.Add(shopDto);
        }

        [HttpGet("{id}")]
        public ShopDto GetById(int id)
        {
            var entity = _repository.GetById(id);

            return _mapper.Map<ShopDto>(entity);
        }


        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _repository.Delete(id);
        }
    }
}
