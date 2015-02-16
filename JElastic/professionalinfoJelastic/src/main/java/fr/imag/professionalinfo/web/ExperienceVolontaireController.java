package fr.imag.professionalinfo.web;

import fr.imag.professionalinfo.domain.ExperienceVolontaire;
import org.springframework.roo.addon.web.mvc.controller.scaffold.RooWebScaffold;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;

@RequestMapping("/experiencevolontaires")
@Controller
@RooWebScaffold(path = "experiencevolontaires", formBackingObject = ExperienceVolontaire.class)
public class ExperienceVolontaireController {
}
