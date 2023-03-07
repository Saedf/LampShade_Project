using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_Framework.Application;
using ShopManagement.Application.Contract.Slider;
using ShopManagement.Domain.SliderAgg;

namespace ShopManagement.Application
{
    public class SliderApplication:ISliderApplication
    {
        private readonly ISliderRepository _sliderRepository;
        private readonly IFileUploader _fileUploader;
        public SliderApplication(ISliderRepository sliderRepository, IFileUploader fileUploader)
        {
            _sliderRepository = sliderRepository;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateSlider command)
        {
            var operation = new OperationResult();
            var pictureName = _fileUploader.Upload(command.Picture, "slides");
            var slider = new Slider(pictureName, command.PictureTitle, command.PictureAlt,
                command.Heading, command.Title, command.Text, command.BtnText,command.Link);
            _sliderRepository.Create(slider);
            _sliderRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditSlider command)
        {
            var operation = new OperationResult();
            var slider = _sliderRepository.GetBy(command.Id);
            if (slider==null)
            {
                return operation.Faild(ApplicationMessage.RecordNotFound);
            }
            var pictureName = _fileUploader.Upload(command.Picture, "slides");

            slider.Edit(pictureName,command.PictureTitle,command.PictureAlt,
                command.Heading,command.Title,command.Text,command.BtnText,command.Link);
            _sliderRepository.SaveChanges();
            return operation.Succeeded();
        }

        public List<SliderViewModel> GetList()
        {
            return _sliderRepository.GetList();
        }

        public OperationResult Remove(long id)
        {
            var operation=new OperationResult();
            var slider = _sliderRepository.GetBy(id);
            if (slider==null)
            {
                return operation.Faild(ApplicationMessage.RecordNotFound);
            }
            slider.Remove();
            _sliderRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();
            var slider = _sliderRepository.GetBy(id);
            if (slider == null)
            {
                return operation.Faild(ApplicationMessage.RecordNotFound);
            }
            slider.Restore();
            _sliderRepository.SaveChanges();
            return operation.Succeeded();
        }

        public EditSlider GetDetails(long id)
        {
            return _sliderRepository.GetDetails(id);
        }
    }
}
