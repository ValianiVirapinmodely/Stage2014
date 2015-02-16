package fr.imag.professionalinfo.web;

import fr.imag.professionalinfo.domain.Cours;
import org.springframework.roo.addon.web.mvc.controller.scaffold.RooWebScaffold;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;

@RequestMapping("/courses")
@Controller
@RooWebScaffold(path = "courses", formBackingObject = Cours.class)
public class CoursController {
}
