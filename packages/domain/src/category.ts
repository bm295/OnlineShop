import type { Product } from "./product";

export type Category = {
  id: number;
  name: string;
  description: string;
  products?: Product[];
};

export type CreateCategoryInput = {
  name: string;
  description: string;
};

export type UpdateCategoryInput = Partial<CreateCategoryInput>;
