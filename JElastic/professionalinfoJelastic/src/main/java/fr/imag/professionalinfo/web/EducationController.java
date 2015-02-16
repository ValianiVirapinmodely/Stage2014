package fr.imag.professionalinfo.web;

import fr.imag.professionalinfo.domain.Education;
import org.springframework.roo.addon.web.mvc.controller.scaffold.RooWebScaffold;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;

@RequestMapping("/educations")
@Controller
@RooWebScaffold(path = "educations", formBackingObject = Education.class)
public class EducationController {
}
