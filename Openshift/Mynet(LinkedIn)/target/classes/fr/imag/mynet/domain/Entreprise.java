package fr.imag.mynet.domain;

import java.util.List;

import org.springframework.social.linkedin.api.CodeAndName;

public class Entreprise {
	private String description;
	private String name;
	private int foundedYear;
	private String employeeCountRangeName;
	private String employeeCountRangeCode;
	private String industry;

	public String getDescription() {
		return description;
	}

	public void setDescription(String description) {
		this.description = description;
	}

	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}

	public int getFoundedYear() {
		return foundedYear;
	}

	public void setFoundedYear(int foundedYear) {
		this.foundedYear = foundedYear;
	}

	public String getEmployeeCountRangeName() {
		return employeeCountRangeName;
	}
	
	public String getEmployeeCountRangeCode() {
		return employeeCountRangeCode;
	}

	public void setEmployeeCountRange(CodeAndName employeeCountRange) {
		this.employeeCountRangeName = employeeCountRange.getName(); // Renvoie "1001-5000"
		this.employeeCountRangeCode = employeeCountRange.getCode(); // Renvoie "G"
	}

	public String getIndustry() {
		return industry;
	}

	public void setIndustry(String industry) {
		this.industry = industry;
	}

	


	
	

}
