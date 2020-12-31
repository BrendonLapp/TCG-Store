using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TCG_Store.Models;
using TCG_Store_DAL.DataAccessControllers;
using TCG_Store_DAL.DTOs;

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
                    Percentage = Quality.Percentage,
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
            FoundQuality.Percentage = QualityDTO.Percentage;

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
                Percentage = NewQuality.Percentage,
                QualityShortName = NewQuality.QualityShortName
            };

            Success = QualityDataController.AddNewQuality(NewQualityDTO);

            return Success;
        }

        [HttpPut]
        public bool Update ([FromBody] Quality UpdatedQuality)
        {
            bool Success;

            QualityDataController QualityDataController = new QualityDataController();

            QualityDTO UpdatingQuality = new QualityDTO
            {
                QualityID = UpdatedQuality.QualityID,
                QualityName = UpdatedQuality.QualityName,
                QualityShortName = UpdatedQuality.QualityShortName,
                Percentage = UpdatedQuality.Percentage
            };

            Success = QualityDataController.UpdateQuality(UpdatingQuality);

            return Success;
        }

        [Route("{QualityID}")]
        [HttpPost]
        public bool Delete (int QualityID)
        {
            bool Success;

            QualityDataController QualityDataController = new QualityDataController();

            Success = QualityDataController.DeleteQuality(QualityID);

            return Success;
        }
    }
}
