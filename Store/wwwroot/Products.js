
const productsList = addEventListener("load", async () => {
    getProducts()
})

const getAllFilters = () => {
    document.getElementById('ProductList').innerHTML = ''
    const filter = {
        minPrice: document.querySelector('#minPrice').value,
        maxPrice: document.querySelector('#maxPrice').value,
        desc: document.querySelector('#nameSearch').value,
        categoryIds: []
    }
    return filter;
}
const getProductsList = async () => {
    let filters = getAllFilters();
    let url = `api/Product/?position=${filters.position}&skip=${filters.skip}`
    if (filters.desc != '')
        url = +`&desc=${filters.desc}`
    if (filters.minPrice != '')
        url = +`&minPrice=${filters.minPrice}`
    if (filters.maxPrice != '')
        url = +`&maxPrice=${filters.maxPrice}`
    if (filters.categoryIds != '')
        url = +`&categoryIds=${filters.categoryIds}`

    try {
        const response = await fetch(url, {
            method: 'Get',
            headers: {
                "Content-Type": "application/json"
            },
            query: position: filters.position, skip: filters.skip, desc: filters.desc,
            minPrice: filters.minPrice, maxPrice: filters.maxPrice, categoryIds: filters.categoryIds
        })

        if (response.status == 204) {
            alert("there is not products")
            productData = []
        }
        else {
            const productData = await response.json()
        }
        drawProducts(productData)
    }
    catch (error) {
        console.log(error)
    }
}



const drawProducts = (products) => {
    for (var i = 0; i < products.length; i++) {
        drawOneProduct(products[i])
    }
}


const drawOneProduct = (product) => {
    let tmp = document.getElementById('temp-card');
    let cloneProduct = tmp.content.cloneNode(true)
    cloneProduct.querySelector('img').src = "./Image/" + product.Image
    cloneProduct.querySelector('h1').textContent = product.Name
    cloneProduct.querySelector('.price').innerText = product.Price
    cloneProduct.querySelector('.description').innerText = product.Description
    cloneProduct.querySelector('.button').addEventListener("click", () => { })
    document.getElementById('ProductList').appendChild(cloneProduct)
}


const drawCategories = (categories) => {
    for (int i = 0; i < categories.length; i++) {
        drawOneCategory(categories[i])
    }
}


const drawOneCategory = (category) => {
    let tmp = document.getElementById('temp-category');
    let cloneCategory = tmp.content.cloneNode(true)
    cloneCategory.querySelector('.opt').addEventListener('checked', () => { })
    cloneCategory.querySelector('.OptionName').textContent = category.Name
    document.getElementById('categoryList').appendChild(cloneCategory)
}