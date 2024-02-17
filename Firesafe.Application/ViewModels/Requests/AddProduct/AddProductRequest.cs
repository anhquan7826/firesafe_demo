using System.ComponentModel.DataAnnotations;
using Google.Apis.Docs.v1.Data;
using Microsoft.AspNetCore.Http;

namespace Application.ViewModels.Requests.AddProduct;

public class AddProductRequest : BaseRequestModel
{
    private readonly string _name = null!;

    [Required]
    public required string Name
    {
        get => _name;
        init => _name = value.Trim();
    }

    [Required] public required string OriginId { get; init; }
    private readonly string _brand = null!;

    [Required]
    public required string Brand
    {
        get => _brand;
        init => _brand = value.Trim();
    }

    private readonly string _model = null!;

    [Required]
    public required string Model
    {
        get => _model;
        init => _model = value.Trim();
    }

    private readonly string _shortDescription = null!;

    [Required]
    public required string ShortDescription
    {
        get => _shortDescription;
        init => _shortDescription = value.Trim();
    }

    [Required] public required bool Available { get; init; }

    [Required] public required short Warranty { get; init; }
    private readonly string _packaging = null!;

    [Required]
    public required string Packaging
    {
        get => _packaging;
        init => _packaging = value.Trim();
    }

    [Required] public required short ShippingTime { get; init; }
    private readonly string _hasSample = null!;

    [Required]
    public required string HasSample
    {
        get => _hasSample;
        init => _hasSample = value.Trim();
    }

    private readonly string _productionRate = null!;

    [Required]
    public required string ProductionRate
    {
        get => _productionRate;
        init => _productionRate = value.Trim();
    }

    private readonly string? _accessories;

    public string? Accessories
    {
        get => _accessories;
        init => _accessories = value?.Trim();
    }

    private readonly string? _postSupport;

    public string? PostSupport
    {
        get => _postSupport;
        init => _postSupport = value?.Trim();
    }

    private readonly string? _faq;

    public string? Faq
    {
        get => _faq;
        init => _faq = value?.Trim();
    }

    private readonly string? _shape;

    public string? Shape
    {
        get => _shape;
        init => _shape = value?.Trim();
    }

    private readonly string? _color;

    public string? Color
    {
        get => _color;
        init => _color = value?.Trim();
    }

    private readonly string? _material;

    public string? Material
    {
        get => _material;
        init => _material = value?.Trim();
    }

    public int? Weight { get; init; }
    public int? Volume { get; init; }
    private readonly string? _structure;

    public string? Structure
    {
        get => _structure;
        init => _structure = value?.Trim();
    }

    private readonly string? _fireResistant;

    public string? FireResistant
    {
        get => _fireResistant;
        init => _fireResistant = value?.Trim();
    }

    private readonly string? _waterResistant;

    public string? WaterResistant
    {
        get => _waterResistant;
        init => _waterResistant = value?.Trim();
    }

    private readonly string? _applications;

    public string? Applications
    {
        get => _applications;
        init => _applications = value?.Trim();
    }

    private readonly string? _additionalSpecification;

    public string? AdditionalSpecification
    {
        get => _additionalSpecification;
        init => _additionalSpecification = value?.Trim();
    }

    public int? Price { get; init; }
    public int? PriceMax { get; init; }
    public int? PriceMin { get; init; }

    private readonly List<string> _categories = null!;

    [Required]
    public required List<string> Categories
    {
        get => _categories;
        init => _categories = value.Select(v => v.Trim()).ToList();
    }

    [Required] public required IFormFile Thumbnail { get; init; }
    public List<IFormFile>? Images { get; init; }
    public List<IFormFile>? Videos { get; init; }
    private readonly List<string> _certificateNames = null!;

    [Required]
    public required List<string> CertificateNames
    {
        get => _certificateNames;
        init => _certificateNames = value.Select(v => v.Trim()).ToList();
    }
    [Required] public required List<IFormFile> CertificateImages { get; init; }
}