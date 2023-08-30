using Microsoft.AspNetCore.Mvc;
using Text_Translator.Models;
using Text_Translator.Repository;

namespace Text_Translator.Controllers
{
    public class TransController : Controller
    {
        private readonly TransRepository _transRepository;

        public TransController(TransRepository transRepository)
        {
            _transRepository = transRepository;
        }

        // GET: Trans
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SaveTranslation()
        {
            return View();
        }

        // ... (other actions)

        // POST: Trans/SaveTranslation
        [HttpPost]
        public IActionResult SaveTranslation(string originalText, string translatedText)
        {
            try
            {
                var translation = new TranslationModel
                {
                    original_text = originalText,
                    translated_text = translatedText,
                    requestedAT = DateTime.Now
                };

                bool result = _transRepository.AddTranslation(translation);

                if (result)
                {
                    return Content("true");
                }
                else
                {
                    return Content("false");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public IActionResult GetAllTranslations()
        {
            try
            {
                // Call the repository method to get all translations
                List<TranslationModel> translations = _transRepository.GetAllTranslations();

                // Check if translations were found
                if (translations != null && translations.Count > 0)
                {
                    // Return the translations as JSON
                    return Json(translations);
                }
                else
                {
                    // Return a message or an empty JSON array if no translations were found
                    return Json(new List<TranslationModel>());
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }

}
