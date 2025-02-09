'use client'

import { Unbounded } from "next/font/google";
import apiClient from "./api";
import Image from "next/image";
import ImageView from "./components/ImageView";
import { useState, useEffect, useCallback } from "react";
import { ImageType } from '@/app/types/imagetype'

const unbounded = Unbounded({ subsets: ['latin'] })

const Home = () => {

  const [image, setImage] = useState<ImageType | null>(null);
  const [error, setError] = useState<Error | null>(null);

  const fetchImage = useCallback(async (id: number) => {
    try {
      const { data } = await apiClient.get(`/Image/${ id }`)
      setImage(data)
    } catch (err) {
      setError(err instanceof Error ? err : new Error(String(err)));
    }
  }, [])

  useEffect(() => {
    fetchImage(1);
  }, [fetchImage]);

  const handleInteraction = async (liked: boolean, id: number) => {
    setImage(null)
    console.log(liked)
    try {
      const { data } = await apiClient.put(`Image/${ id }`, {
        liked
      })

      setImage(data)
    } catch (err) {
      setError(err instanceof Error ? err : new Error(String(err)));
    }
  }
  
  if(error) {
    return (
      <div>
        Error
      </div>
    )
  }

  return (
    <div className={ unbounded.className }>
      <div className="grid grid-rows-[20px_1fr_20px] items-center justify-items-center min-h-screen p-8 pb-20 gap-8">
        <h1>
          <Image
            src="/logo.webp"
            height={61}
            width={250}
            alt="Arquiteto de Bolso - design de vida"
          />
        </h1>
        {
          image && (
            <main className="flex flex-col row-start-2 gap-4 items-center sm:items-start">
              <ImageView image={ image } onInteraction={handleInteraction} />
            </main>
          )
        }
      </div>
    </div>
  );
}

export default Home;