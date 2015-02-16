package fr.imag.professionalinfo.web;

import fr.imag.professionalinfo.domain.Position_;
import org.springframework.roo.addon.web.mvc.controller.scaffold.RooWebScaffold;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;

@RequestMapping("/position_s")
@Controller
@RooWebScaffold(path = "position_s", formBackingObject = Position_.class)
public class Position_Controller {
}
