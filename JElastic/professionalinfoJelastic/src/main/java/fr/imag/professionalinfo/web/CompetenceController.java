package fr.imag.professionalinfo.web;

import fr.imag.professionalinfo.domain.Competence;
import org.springframework.roo.addon.web.mvc.controller.scaffold.RooWebScaffold;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;

@RequestMapping("/competences")
@Controller
@RooWebScaffold(path = "competences", formBackingObject = Competence.class)
public class CompetenceController {
}
