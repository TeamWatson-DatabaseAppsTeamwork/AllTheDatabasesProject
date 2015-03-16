for /f %%f	in ('dir /b C:\productsSystem\salesByVendor') do mongoimport --db productsSystem --collection salesByVendor < C:\productsSystem\salesByVendor\%%f
PAUSE