import type { Category } from "./category";

export type Product = {
  id: number;
  name: string;
  sku: string;
  price: number;
  stockQuantity: number;
  categoryId: number;
  category?: Category | null;
};

export type CreateProductInput = {
  name: string;
  sku: string;
  price: number;
  stockQuantity: number;
  categoryId: number;
};

export type UpdateProductInput = Partial<CreateProductInput>;
