import { z } from "zod";

import type { Category } from "./category";
import { productSchema } from "./product.schema";

export const createCategorySchema = z.object({
  name: z.string().trim().min(1),
  description: z.string(),
});

export const categorySchema: z.ZodType<Category> = createCategorySchema
  .extend({
    id: z.number().int().positive(),
    products: z.array(z.lazy(() => productSchema)).optional(),
  })
  .transform(({ products, ...category }) =>
    products === undefined ? category : { ...category, products },
  );
