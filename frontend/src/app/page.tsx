'use client'

import Image from "next/image";
import IterationIcon  from './components/IterationIcon' 

export default function Home() {
  const handleClickIteration = (icon: string) => {
    alert(icon)
  }

  return (
    <div className="grid grid-rows-[20px_1fr_20px] items-center justify-items-center min-h-screen p-8 pb-20 gap-8">
      <h1>Match Upik</h1>
      <main className="flex flex-col row-start-2 gap-4 items-center sm:items-start">
        <div className="w-full">
          <div className="flex justify-between">
            <strong>Titulo</strong>
            <small>x likes</small>
          </div>
          <p className="text-sm text-justify text-slate-600">
            Lorem ipsum dolor sit amet consectetur adipisicing elit. Sequi ad maiores numquam placeat temporibus, autem perferendis alias, dolor cumque magnam quidem, rerum quae unde reiciendis a. Eius expedita dolores veritatis?
          </p>
        </div>
        <Image
          src="https://placehold.co/600x400.png" 
          alt="Image Title"
          width={600}
          height={400}
        />
        <div className="w-full justify-center flex gap-4 -translate-y-11">
          <IterationIcon icon="cancel" onClick={handleClickIteration} />
          <IterationIcon icon="favourite" onClick={handleClickIteration} />
        </div>
      </main>
    </div>
  );
}
