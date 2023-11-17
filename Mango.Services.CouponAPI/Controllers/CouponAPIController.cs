using AutoMapper;
using Azure;
using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CouponAPI.Controllers
{
    [Route("api/coupon")]
    [ApiController]
    public class CouponAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly ResponseDto _response;
        private IMapper _mapper;

        public CouponAPIController(AppDbContext db, ResponseDto reponse, IMapper mapper)
        {
            _db = db;
            _response=new ResponseDto();
            _mapper=mapper; 
        }

        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                IEnumerable<Coupon> coupons = _db.Coupons.ToList();
                _response.Result=_mapper.Map<IEnumerable<CouponDto>>(coupons);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;

        }
        [HttpGet]
        [Route("{id:int}")]
        public ResponseDto Get(int id)
        {
            try
            {
                Coupon coupon = _db.Coupons.First(x=>x.CouponId==id);
                //CouponDto couponDto = new CouponDto()
                //{
                //    CouponId = coupon.CouponId,
                //    CouponCode = coupon.CouponCode,
                //    DiscountAmount = couponDto.DiscountAmount,
                //    MinAmount = couponDto.MinAmount,
                //};
                _response.Result=_mapper.Map<CouponDto>(coupon);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message=ex.Message;    

            }
            return _response;

        }
        [HttpGet]
        [Route("GetByCode/{code}")]
        public ResponseDto GetByCode(string code)
        {
            try
            {
                Coupon coupon = _db.Coupons.First(x => x.CouponCode.ToLower() == code.ToLower());
            
                _response.Result = _mapper.Map<CouponDto>(coupon);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;

            }
            return _response;

        }

        [HttpPost]
        public ResponseDto Post([FromBody] CouponDto couponDto)
        {
            try
            {
                Coupon obj=_mapper.Map<Coupon>(couponDto); 
                _db.Coupons.Add(obj);
                _db.SaveChanges();

                _response.Result = _mapper.Map<CouponDto>(obj);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;

            }
            return _response;

        }
        [HttpPut]
        public ResponseDto Put([FromBody] CouponDto couponDto)
        {
            try
            {
                Coupon obj = _mapper.Map<Coupon>(couponDto);
                _db.Coupons.Update(obj);
                _db.SaveChanges();

                _response.Result = _mapper.Map<CouponDto>(obj);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;

            }
            return _response;

        }
        [HttpDelete]
        [Route("{id:int}")]
        public ResponseDto Delete( int id )
        {
            try
            {
                Coupon obj = _db.Coupons.First(u=>u.CouponId == id);    
                _db.Coupons.Remove(obj);
                _db.SaveChanges();

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;

            }
            return _response;

        }
    }
}
