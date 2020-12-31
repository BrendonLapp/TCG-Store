using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TCG_Store.Models;
using TCG_Store_DAL.DTOs;
using TCG_Store_DAL.DataAccessControllers;

namespace TCG_Store.Controllers
{
    [Route("api/v1/Quality")]
    [ApiController]
    public class QualityController : ControllerBase
    {
        [HttpGet]
        public List<Quality> Get()
        {
            List<Quality> AllQualities = new List<Quality>();
            List<QualityDTO> QualityDTOs = new List<QualityDTO>();
            QualityDataController QualityDataController = new QualityDataController();

            QualityDTOs = QualityDataController.GetAllQualities();

            foreach (var Quality in QualityDTOs)
            {
                Quality IncomingQuality = new Quality
                {
                    QualityID = Quality.QualityID,
                    QualityName = Quality.QualityName,
                    QualityPercentage = Quality.QualityPercentage,
                    QualityShortName = Quality.QualityShortName
                };
                AllQualities.Add(IncomingQuality);
            }
            return AllQualities;
        }

        [HttpGet("{QualityID}")]
        public Quality Get (int QualityID)
        {
            Quality FoundQuality = new Quality();
            QualityDTO QualityDTO = new QualityDTO();
            QualityDataController QualityDataController = new QualityDataController();

            QualityDTO = QualityDataController.GetQualityByID(QualityID);

            FoundQuality.QualityID = QualityDTO.QualityID;
            FoundQuality.QualityName = QualityDTO.QualityName;
            FoundQuality.QualityShortName = QualityDTO.QualityShortName;
            FoundQuality.QualityPercentage = QualityDTO.QualityPercentage;

            return FoundQuality;
        }

        [HttpPost]
        public bool Post ([FromBody] Quality NewQuality)
        {
            bool Success;
            
            QualityDataController QualityDataController = new QualityDataController();

            QualityDTO NewQualityDTO = new QualityDTO
            {
                QualityName = NewQuality.QualityName,
                QualityPercentage = NewQuality.QualityPercentage,
                QualityShortName = NewQuality.QualityShortName
            };

            Success = QualityDataController.AddNewQuality(NewQualityDTO);

            return Success;
        }

        [HttpPut]
        public bool Update([FromBody] Quality UpdatedQuality)
        {

        }
    }
}
