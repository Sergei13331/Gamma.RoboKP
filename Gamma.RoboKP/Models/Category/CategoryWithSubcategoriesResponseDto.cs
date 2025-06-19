namespace Gamma.RoboKP.Models.Category;

public record CategoryWithSubcategoriesResponseDto(

     long Id ,
     string Name, 
    List<SubcategoryInnerResponseDto> InnerResponse 
);