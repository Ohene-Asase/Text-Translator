namespace Text_Translator.Models
{
    public class TranslationModel
    {
        public int translation_id { get; set; } 
        public string original_text { get; set; }    
        public string translated_text { get; set; } 

        public DateTime requestedAT { get; set; }
    }
}
